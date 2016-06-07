#ifndef _MONSTER_ACTOR_H
#define _MONSTER_ACTOR_H

#include "monster_input.h"
#include "actor_base.hpp"

namespace pacman {

    class monster_actor
        : public actor_base<monster_actor> {
    public:

        typedef monster_input input_type;

    private:

        input_type _input;

    public:

        monster_actor(coordinates const & p);

        monster_actor(monster_actor const & other);

        virtual input_type & get_input();
    };
}

#endif //_MONSTER_ACTOR_H
