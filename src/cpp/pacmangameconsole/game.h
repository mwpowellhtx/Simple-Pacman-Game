#ifndef _GAME_H_
#define _GAME_H_

#include "board.h"
#include "pacman_actor.h"
#include "monster_actor.h"

#include <vector>
#include <memory>
#include <ostream>

namespace pacman {

    class game {

        board _board;

        pacman_actor _pacman;

        std::vector<monster_actor> _monsters;

    public:

        game(coordinates const & pacman_coords
            , std::vector<std::string> const & mapped);

        board & get_board();

        pacman_actor & get_pacman();

        std::vector<monster_actor> get_monsters();

        move_choice monster_input_callback(positioned const & p);

        game & next();

        void update();

        std::ostream & report(std::ostream & os);

        bool is_won();
        bool is_game_over();

    private:

        void pacman_moving(pacman_actor & pacman);
        void monster_moving(monster_actor & monster);

        bool _gameOver;

        std::unique_ptr<bool> _won;

        void set_won(bool value);

        bool try_test_pacman_position();
        bool try_test_won();
        bool try_test_monster_positions();
    };
}

#endif //_GAME_H_
