#include "monster_actor.h"

namespace pacman {

    monster_actor::monster_actor(coordinates const & p)
        : actor_base(p)
        , _input() {
    }

    monster_actor::monster_actor(monster_actor const & other)
        : actor_base(other._position)
        , _input() {
    }

    monster_actor::input_type & monster_actor::get_input() {
        return _input;
    }
}
