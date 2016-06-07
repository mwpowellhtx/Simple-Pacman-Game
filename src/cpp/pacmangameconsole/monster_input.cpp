#include "monster_input.h"

#include <algorithm>

namespace pacman {

    monster_input::monster_input()
        : input_base([](positioned const &) { return none; }) {
    }

    monster_input::monster_input(monster_input const & other)
        : input_base([](positioned const &) { return none; }) {
    }
}
