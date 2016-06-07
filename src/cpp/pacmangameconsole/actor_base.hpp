#ifndef _ACTOR_BASE_HPP_
#define _ACTOR_BASE_HPP_

#include "positioned.h"

#include <functional>
#include <vector>

namespace pacman {

    template<
        class Derived
            , class Input
    >
    class actor_base
        : public positioned {
    public:

        typedef Input input_type;
        typedef Derived derived_type;

    private:

        input_type _input;

    public:

        bool alive;

        // TODO: TBD: a proper signal/slot would be better here: i.e. Boost.Signals2
        std::vector<std::function<void(derived_type &)>> _moving;

    protected:

        actor_base(coordinates const & p)
            : positioned(p)
            , _input()
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

        virtual Input & get_input() {
            return _input;
        }

        virtual void moving() {
            for (auto & callback : _moving) {
                callback(dynamic_cast<derived_type &>(*this));
            }
        }
    };
}

#endif //_ACTOR_BASE_HPP_
