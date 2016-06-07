#include "input_path.h"

namespace pacman {

    input_path::input_path(coordinates curr
        , coordinates dest)
        : curr(curr)
        , dest(dest)
        , hist()
        , vist() {

        hist.push_back(none);
        vist[curr] = none;
    }

    input_path::input_path(coordinates curr
        , coordinates dest
        , std::vector<move_choice> const & hist
        , std::map<coordinates, move_choice> const & vist)
        : curr(curr)
        , dest(dest)
        , hist(hist)
        , vist(vist) {
    }

    input_path::input_path(input_path const & other)
        : curr(other.curr)
        , dest(other.dest)
        , hist(other.hist)
        , vist(other.vist) {
    }

    bool input_path::is_found() const {
        return is_equal(curr, dest);
    }

    input_path input_path::clone() {
        return input_path(*this);
    }
}
