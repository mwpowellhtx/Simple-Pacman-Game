#ifndef _PACMAN_ACTOR_H
#define _PACMAN_ACTOR_H

#include "pacman_input.h"
#include "actor_base.hpp"

#include <functional>

namespace pacman {

    class pacman_actor
        : public actor_base<pacman_actor> {
    public:

        typedef pacman_input input_type;

    private:

        input_type _input;

    public:

        pacman_actor(coordinates const & p);

        pacman_actor(pacman_actor const & other);

        input_type & get_input();
    };
}

#endif //_PACMAN_ACTOR_H
