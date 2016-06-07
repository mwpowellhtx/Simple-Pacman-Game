#include "input_base.h"

namespace pacman {

    input_base::input_base(callback_type const & callback)
        : _callback(callback) {
    }

    move_choice input_base::next(positioned const & p) {
        return _callback(p);
    }

    void input_base::set_callback(callback_type const & callback) {
        _callback = callback;
    }
}
