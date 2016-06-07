#ifndef _PACMAN_INPUT_H_
#define _PACMAN_INPUT_H_

#include "input_base.h"

namespace pacman {

    class pacman_input
        : public input_base {
    public:

        pacman_input();

        pacman_input(pacman_input const & other);

    private:

        move_choice get_input(positioned const &);
    };
}

#endif //_PACMAN_INPUT_
