#include "enums.h"

#include <set>

namespace pacman {

    board_state to_board_state(char value) {
        return static_cast<board_state>(value);
    }

    bool is_torsoidal(board_state value) {
        const static std::set<board_state> values{ one, two };
        return values.find(value) != values.end();
    }

    std::vector<move_choice> get_move_choices() {
        static std::vector<move_choice> choices = { up, down, left, right };
        return choices;
    }
}
