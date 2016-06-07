#include "monster_actor.h"

namespace pacman {

    monster_actor::monster_actor(coordinates const & p, const char & name)
        : actor_base(p)
        , _name(name) {
    }

    monster_actor::monster_actor(monster_actor const & other)
        : actor_base(other._position)
        , _name(other._name) {
    }
}
