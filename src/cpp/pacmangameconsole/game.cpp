#include "game.h"

#include <algorithm>

#include "element.hpp"
#include "linq.hpp"

namespace pacman {

    game::game(coordinates const & pacman_coords
        , std::vector<std::string> const & mapped)
        : _board(mapped)
        , _pacman(pacman_coords)
        , _monsters()
        , _gameOver(false)
        , _won() {

        _pacman.on_moving(std::bind(&game::pacman_moving, this, std::placeholders::_1));

        coordinates monster_coords(_board.get_width() - 2, _board.get_height() - 2);

        _monsters = {
            monster_actor(monster_coords),
            monster_actor(monster_coords),
            monster_actor(monster_coords)
        };

        for (auto & m : _monsters) {

            m.get_input().set_callback(
                std::bind(&game::monster_input_callback, this, std::placeholders::_1));

            m.on_moving(
                std::bind(&game::monster_moving, this, std::placeholders::_1));
        }
    }

    void game::pacman_moving(pacman_actor & pacman) {
        auto & b = get_board();
        auto choice = pacman.get_input().next(pacman);
        auto applied = apply(pacman._position, b, choice);
        if (b.get_state(applied) != wall) {
            pacman._position = applied;
            b.eat(applied);
        }
    }

    void game::monster_moving(monster_actor & monster) {
        auto & b = get_board();
        //TODO: TBD: also assumes that we have done some QA on the board cells, valid paths, etc...
        auto choice = monster.get_input().next(monster);
        // In which case, we are guaranteed for the monster choices not to run into any walls.
        monster._position = apply(monster._position, b, choice);
    }

    board & game::get_board() {
        return _board;
    }

    pacman_actor & game::get_pacman() {
        return _pacman;
    }

    std::vector<monster_actor> game::get_monsters() {
        return _monsters;
    }

    move_choice game::monster_input_callback(positioned const & p) {

        auto b = get_board();

        auto choices = get_move_choices();

        std::vector<move_choice> possible;

        // Help restrict the values we want. Otherwise the monster logic will be indecisive.
        std::copy_if(choices.cbegin(), choices.cend(), std::back_inserter(possible), [&](move_choice const & c) {
            // This part should be consistent regardless whether talking about a torsoidal or normal movement.
            auto applied = apply(p._position, b, c);
            return b.get_state(applied) != wall;
        });

        // We can inject a choice in for the monster or select one at random.
        auto choice = cxx::_element_at_random(possible);

        return choice;
    }

    bool has_monster(std::vector<monster_actor> const & monsters, coordinates const & coords) {
        for (const auto & m : monsters) {
            if (is_equal(m._position, coords)) {
                return true;
            }
        }
        return false;
    }

    game & game::next() {

        _pacman.moving();

        if (try_test_pacman_position()
            || try_test_won()
            || !try_test_monster_positions()) {

            return *this;
        }

        set_won(false);

        return *this;
    }

    void game::set_won(bool value) {
        _won = std::make_unique<bool>(value);
    }

    bool game::is_game_over() {
        return _gameOver;
    }

    bool game::is_won() {
        if (_board.can_win()) {
            set_won(true);
        }
        return _won == nullptr ? false : *_won;
    }

    bool game::try_test_pacman_position() {
        return _gameOver = linq::any<monster_actor>(_monsters,
            [&](monster_actor const & m) {
                return is_equal(m._position, _pacman._position);
        });
    }

    bool game::try_test_won() {
        return _gameOver = is_won();
    }

    bool game::try_test_monster_positions() {
        // Defer to the Monster(s) move(s).
        for (auto & m : _monsters) {
            m.moving();
        }
        auto & gameOver = _gameOver = linq::any<monster_actor>(_monsters,
            [&](monster_actor const & m) {
                return is_equal(m._position, _pacman._position);
        });
        _pacman.alive = !gameOver;
        return gameOver;
    }

    std::ostream & game::report(std::ostream & os) {

        update();

        std::string message;

        if (is_game_over()) {
            message = is_won() ? "GAME OVER YOU WON!" : "GAME OVER YOU LOST";
        }

        get_board().report(os);

        if (message.length()) {
            os << message << std::endl;
        }

        os << std::endl << std::flush;

        return os;
    }

    void game::update() {

        auto & b = get_board();

        const auto & pacman = get_pacman();
        const auto & monsters = get_monsters();

        const auto & states = b.get_states();

        std::vector<coordinates> keys;

        for (const auto & s : states) {
            keys.push_back(s.first);
        }

        // This step is critical to achieve the correct mapped order.
        std::sort(keys.begin(), keys.end(), [](coordinates & a, coordinates & b) { return a < b; });

        auto & mapped = b.get_mapped();

        mapped.clear();

        for (const auto & k : keys) {

            char ch = 0;

            //P = Pac-Man
            //M = Monster
            //# = Wall
            //* = Pill
            if (is_equal(k, pacman._position)) {
                ch = pacman.alive ? 'P' : 'X';
            }
            else if (has_monster(monsters, k)) {
                ch = 'M';
            }
            else {
                ch = static_cast<char>(states.at(k));
            }

            if (mapped.size() == k._y) {
                mapped.push_back(std::string());
            }

            mapped[mapped.size() - 1] += ch;
        }
    }
}
