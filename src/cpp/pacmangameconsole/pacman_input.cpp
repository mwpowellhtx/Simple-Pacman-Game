#include "pacman_input.h"

#include <cctype>
#include <conio.h>

namespace pacman {

    pacman_input::pacman_input()
        : input_base(std::bind(&pacman_input::get_input, this, std::placeholders::_1)) {
    }

    pacman_input::pacman_input(pacman_input const & other)
        : input_base(std::bind(&pacman_input::get_input, this, std::placeholders::_1)) {
    }

    move_choice pacman_input::get_input(positioned const & p) {
        switch (tolower(_getch())) {
        case 'a': return left;
        case 'd': return right;
        case 'w': return up;
        case 's': return down;
        }
        return none;
    }
}
