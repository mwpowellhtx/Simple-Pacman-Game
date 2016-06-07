#include "positioned.h"

namespace pacman {

    positioned::positioned()
        : _position() {
    }

    positioned::positioned(coordinates const & p)
        : _position(p) {
    }

    positioned::~positioned() {
    }
}
