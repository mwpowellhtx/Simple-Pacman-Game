#ifndef _ENUMS_H_
#define _ENUMS_H_

#include <vector>

namespace pacman {

    enum board_state : char {
        one = '1'
        , two = '2'
        , wall = '#'
        , pill = '*'
        , empty = ' '
    };

    extern bool is_torsoidal(board_state value);

    enum move_choice {
        none
        , up
        , down
        , left
        , right
    };

    extern std::vector<move_choice> get_move_choices();

    extern board_state to_board_state(char value);
}

#endif //_ENUMS_H_
