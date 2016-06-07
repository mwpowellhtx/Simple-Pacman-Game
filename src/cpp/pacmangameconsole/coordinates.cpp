#include "coordinates.h"

namespace pacman {

    coordinates::coordinates()
        : cloneable()
        , equatable()
        , _x(0)
        , _y(0) {
    }

    coordinates::coordinates(size_t x, size_t y)
        : cloneable()
        , equatable()
        , _x(x)
        , _y(y) {
    }

    coordinates::coordinates(coordinates const & other)
        : cloneable()
        , equatable()
        , _x(other._x)
        , _y(other._y) {
    }

    bool is_equal(coordinates const & a, coordinates const & b) {
        return a._x == b._x && a._y == b._y;
    }

    bool coordinates::equals(equatable_type const & other) {
        return is_equal(*this, other);
    }

    bool operator<(coordinates const & a, coordinates const & b) {
        // We want rows first followed by columns.
        if (a._y < b._y) {
            return true; 
        }
        else if (a._y > b._y) {
            return false;
        }
        // Test for X when Y is equal.
        return a._x < b._x;
    }

    coordinates coordinates::operator=(coordinates const & other) {
        _x = other._x;
        _y = other._y;
        return *this;
    }
}
