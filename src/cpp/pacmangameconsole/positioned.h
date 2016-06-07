#ifndef _POSITIONED_H_
#define _POSITIONED_H_

#include "coordinates.h"

namespace pacman {

    class positioned {
    protected:

        positioned();

        positioned(coordinates const & p);

    public:

        ~positioned();

        coordinates _position;
    };
}

#endif //_POSITIONED_H_
