#include "monster_actor.h"

namespace pacman {

    monster_actor::monster_actor(coordinates const & p)
        : actor_base(p) {
    }

    monster_actor::monster_actor(monster_actor const & other)
        : actor_base(other._position) {
    }
}
