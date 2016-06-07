#ifndef _INPUT_BASE_H_
#define _INPUT_BASE_H_

#include "enums.h"
#include "positioned.h"

#include <functional>

namespace pacman {

    class input_base {
    public:

        typedef std::function<move_choice(positioned const &)> callback_type;

    protected:

        callback_type _callback;

        input_base(callback_type const & callback);

    public:

        move_choice next(positioned const & p);

        void set_callback(callback_type const & callback);
    };
}

#endif //_INPUT_BASE_H_
