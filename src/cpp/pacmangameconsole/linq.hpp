#ifndef _LINQ_HPP_
#define _LINQ_HPP_

#include <vector>
#include <map>
#include <functional>

namespace linq {

    template<typename T>
    bool any(std::vector<T> const & values, std::function<bool(T const &)> const & pred) {
        for (const auto & value : values) {
            if (pred(value)) {
                return true;
            }
        }
        return false;
    }

    template<typename K, typename V>
    bool any(std::map<K, V> const & values, std::function<bool(std::pair<K, V> const &)> const & pred) {
        for (const auto & value : values) {
            if (pred(value)) {
                return true;
            }
        }
        return false;
    }
}

#endif //_LINQ_HPP_
