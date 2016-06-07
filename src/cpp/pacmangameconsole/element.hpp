#ifndef _ELEMENT_HPP_
#define _ELEMENT_HPP_

#include <vector>
#include <cstdlib>

namespace cxx {

    template<typename T, typename size_type>
    T const & _element_at(std::vector<T> const & values, size_type i) {
        return values[i];
    }

    template<typename T>
    T const & _element_at_random(std::vector<T> const & values) {
        auto i = std::rand() % values.size();
        return _element_at(values, i);
    }
}

#endif //_ELEMENT_HPP_
