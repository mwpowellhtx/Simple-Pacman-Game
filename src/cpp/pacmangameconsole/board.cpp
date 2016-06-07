#include "board.h"

#include <functional>
#include <algorithm>
#include <memory>

#include "linq.hpp"

namespace pacman {

    board::board(std::vector<std::string> const & mapped)
        : _mapped(mapped)
        , _states()
        , _height(0)
        , _width(0) {

        parse_mapped();
    }

    void board::parse_mapped() {

        _height = _mapped.size();

        for (auto const & s : _mapped) {
            _width = std::max(_width, s.length());
        }

        // Instead of dealing with one line at a time, just accept all of them in sequence.
        for (std::vector<std::string>::size_type j = 0; j < _mapped.size(); j++) {
            for (std::string::size_type i = 0; i < _mapped[j].length(); i++) {
                coordinates coord(i, j);
                auto state = to_board_state(_mapped[j][i]);
                _states[coord] = state;
            }
        }
    }

    board_state board::get_state(coordinates const & coords) const {
        return _states.at(coords);
    }

    void board::eat(coordinates const & coords) {
        auto state = _states[coords];
        // We only want to eat pill positions. Leave torsoidal positions alone.
        if (state == pill) _states[coords] = empty;
    }

    size_t board::get_height() const {
        return _height;
    }

    size_t board::get_width() const {
        return _width;
    }

    std::map<coordinates, board_state> board::get_states() const {
        return _states;
    }

    coordinates find_torsoidal_coords(board const & b, coordinates const & coords) {

        const auto state = b.get_state(coords);

        if (is_torsoidal(state)) {
            return coordinates();
        }

        auto const & states = b.get_states();

        std::vector<std::pair<coordinates, board_state>> placeholder;

        std::copy_if(states.cbegin(), states.cend(), placeholder.begin(),
            [&coords, &state](std::pair<coordinates, board_state> const & s) {
            return s.second == state && !is_equal(s.first, coords);
        });

        return placeholder[0].first;
    }

    coordinates apply(coordinates const & coords, board const & b, move_choice choice) {

        auto result = coords.clone();

        auto find = [&]() {
            return find_torsoidal_coords(b, result);
        };

        switch (choice) {
        case left:
            if (result._x == 0) {
                return find();
            }
            result._x--;
            break;

        case right:
            if (result._x == b.get_width() - 1) {
                return find();
            }
            result._x++;
            break;

        case up:
            if (result._y == 0) {
                return find();
            }
            result._y--;
            break;

        case down:
            if (result._y == b.get_height() - 1) {
                return find();
            }
            result._y++;
            break;

        default:
            break;
        }

        return result;
    }

    bool board::can_win() const {
        auto predicate = [](std::pair<coordinates, board_state> const & p) {
            return p.second == pill;
        };
        return !linq::any<coordinates, board_state>(_states, predicate);
    }

    std::vector<std::string> & board::get_mapped() {
        return _mapped;
    }

    std::ostream & board::report(std::ostream& os) {
        for (const auto & m : _mapped) {
            os << m << std::endl;
        }
        return os;
    }
}
