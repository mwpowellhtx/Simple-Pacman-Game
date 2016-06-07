#ifndef _CLONEABLE_HPP_
#define _CLONEABLE_HPP_

namespace cxx {

    template<class Derived>
    class cloneable {
    public:

        typedef Derived cloneable_type;

    protected:

        cloneable() {
        }

    public:

        virtual cloneable_type clone() const {
            return cloneable_type(dynamic_cast<cloneable_type const &>(*this));
        }
    };
}

#endif //_CLONEABLE_HPP_
