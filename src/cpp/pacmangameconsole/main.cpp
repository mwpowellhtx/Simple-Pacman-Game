#include "game.h"

#include <cstdlib>
#include <ctime>
#include <iostream>
#include <conio.h>

std::vector<std::string> get_small_map() {
    std::vector<std::string> mapped = {
        "######1###",
        "# *******#",
        "2*##**##*#",
        "#********#",
        "#*#*##*#*#",
        "#*#*##*#*#",
        "#********#",
        "#*##**##*2",
        "#********#",
        "###1######"
    };
    return mapped;
}

int main() {

    // Initializes the random seed.
    std::srand(std::time(nullptr));

    pacman::game g(
        pacman::coordinates(1, 1)
        , get_small_map());

    do
    {
        g.report(std::cout);
        g.next();
    } while (!g.is_game_over());

    g.report(std::cout);

    std::cout << "Press any key to continue..." << std::flush;
    _getch();

    return 0;
}
