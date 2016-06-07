#ifndef _EQUATABLE_HPP_
#define _EQUATABLE_HPP_

namespace cxx {

    template<class Derived>
    class equatable {
    public:

        typedef Derived equatable_type;

    protected:

        equatable() {
        }

    public:

        virtual bool equals(equatable_type const & other) = 0;
    };
}

#endif //_EQUATABLE_HPP_
