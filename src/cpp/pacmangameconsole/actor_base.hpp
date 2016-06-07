#ifndef _ACTOR_BASE_HPP_
#define _ACTOR_BASE_HPP_

#include "positioned.h"
#include "input_base.h"

#include <functional>
#include <vector>

namespace pacman {

    template<
        class Derived
    >
    class actor_base
        : public positioned {
    public:

        typedef input_base input_type;
        typedef Derived derived_type;

    public:

        bool alive;

        // TODO: TBD: a proper signal/slot would be better here: i.e. Boost.Signals2
        std::vector<std::function<void(derived_type &)>> _moving;

    protected:

        actor_base(coordinates const & p)
            : positioned(p)
            , _moving()
            , alive(true) {
        }

        ~actor_base() {
            _moving.clear();
        }

    public:

        void on_moving(std::function<void(derived_type &)> const & callback) {
            _moving.push_back(callback);
        }

        virtual input_type & get_input() = 0;

        virtual void moving() {
            for (auto & callback : _moving) {
                callback(dynamic_cast<derived_type &>(*this));
            }
        }
    };
}

#endif //_ACTOR_BASE_HPP_
