#ifndef _COORDINATES_H_
#define _COORDINATES_H_

#include "cloneable.hpp"
#include "equatable.hpp"

#include <functional>

namespace pacman {

    class coordinates
        : public cxx::equatable<coordinates>
        , public cxx::cloneable<coordinates>
        , public std::less<coordinates> {
    
    public:

        size_t _x, _y;

        coordinates();

        coordinates(size_t x, size_t y);

        coordinates(coordinates const & other);

        virtual bool equals(equatable_type const & other);

        coordinates operator=(coordinates const & other);
    };

    bool is_equal(coordinates const & a, coordinates const & b);

    bool operator<(coordinates const & a, coordinates const & b);
}

#endif //_COORDINATES_H_
