#ifndef _INPUT_PATH_H_
#define _INPUT_PATH_H_

#include "enums.h"
#include "coordinates.h"

#include <vector>
#include <map>

#include "cloneable.hpp"

namespace pacman {

    struct input_path
        : public cxx::cloneable<input_path> {
    public:

        input_path(coordinates curr
            , coordinates dest);

        input_path(coordinates curr
            , coordinates dest
            , std::vector<move_choice> const & hist
            , std::map<coordinates, move_choice> const & vist);

        input_path(input_path const & other);

        coordinates curr;

        coordinates dest;

        std::vector<move_choice> hist;

        std::map<coordinates, move_choice> vist;

        bool is_found() const;

    public:

        virtual input_path clone();
    };
}

#endif //_INPUT_PATH_H_
