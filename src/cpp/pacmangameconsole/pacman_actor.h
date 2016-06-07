#ifndef _PACMAN_ACTOR_H
#define _PACMAN_ACTOR_H

#include "pacman_input.h"
#include "actor_base.hpp"

namespace pacman {

    class pacman_actor
        : public actor_base<pacman_actor, pacman_input> {
    public:

        pacman_actor(coordinates const & p);

        pacman_actor(pacman_actor const & other);
    };
}

#endif //_PACMAN_ACTOR_H
