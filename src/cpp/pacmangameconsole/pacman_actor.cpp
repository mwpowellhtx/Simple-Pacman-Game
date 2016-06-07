#include "pacman_actor.h"

namespace pacman {

    pacman_actor::pacman_actor(coordinates const & p)
        : actor_base(p)
        , _input() {
    }

    pacman_actor::pacman_actor(pacman_actor const & other)
        : actor_base(other._position)
        , _input() {
    }

    pacman_actor::input_type & pacman_actor::get_input() {
        return _input;
    }
}
