#include "pacman_actor.h"

namespace pacman {

    pacman_actor::pacman_actor(coordinates const & p)
        : actor_base(p) {
    }

    pacman_actor::pacman_actor(pacman_actor const & other)
        : actor_base(other._position) {
    }
}
