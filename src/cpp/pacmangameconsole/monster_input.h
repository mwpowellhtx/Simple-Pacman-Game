#ifndef _MONSTER_INPUT_H_
#define _MONSTER_INPUT_H_

#include "input_base.h"

namespace pacman {

    class monster_input
        : public input_base {
    public:

        monster_input();

        monster_input(monster_input const & other);
    };
}

#endif //_MONSTER_INPUT_H_
