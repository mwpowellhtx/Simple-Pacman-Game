#include "enums.h"

namespace pacman {

    board_state to_board_state(char value) {
        return static_cast<board_state>(value);
    }

    bool is_torsoidal(board_state value) {
        return value == one || value == two;
    }

    std::vector<move_choice> get_move_choices() {
        static std::vector<move_choice> choices = { up, down, left, right };
        return choices;
    }
}
