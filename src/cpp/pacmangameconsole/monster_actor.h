#ifndef _MONSTER_ACTOR_H
#define _MONSTER_ACTOR_H

#include "monster_input.h"
#include "actor_base.hpp"

namespace pacman {

    class monster_actor
        : public actor_base<monster_actor, monster_input> {
    public:

        char _name;

        monster_actor(coordinates const & p, const char & name);

        monster_actor(monster_actor const & other);
    };
}

#endif //_MONSTER_ACTOR_H
