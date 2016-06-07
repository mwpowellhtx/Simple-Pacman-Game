#ifndef _BOARD_H_
#define _BOARD_H_

#include "coordinates.h"
#include "enums.h"

#include <string>
#include <map>
#include <vector>
#include <ostream>

namespace pacman {

    class board {
    private:

        std::vector<std::string> _mapped;

        std::map<coordinates, board_state> _states;

        size_t _height, _width;

    public:

        board(std::vector<std::string> const & mapped);

        board_state get_state(coordinates const & coords) const;

        void eat(coordinates const & coords);

        size_t get_height() const;
        size_t get_width() const;

        std::map<coordinates, board_state> get_states() const;

        std::vector<std::string> & get_mapped();

        bool can_win() const;

    private:

        void parse_mapped();

    public:

        std::ostream & report(std::ostream & os);
    };

    extern coordinates apply(coordinates const & coords, board const & b, move_choice choice = none);
}

#endif //_BOARD_H_
