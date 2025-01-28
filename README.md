# Shark'Eat! 🦈🍴

<p align="center">
  <img src="https://imgur.com/jmGvCG5" alt="Shark Eat Logo" width="600">
</p>

## 🏆 Awards & Team Shout-Out

**Shark'Eat!** proudly earned **2 awards** at the **Global Game Jam 2025 (Neapolis, Vilanova i la Geltrú)**:
- **Most Original Concept**  
- **Best Use of Diversifiers**  

And it’s all thanks to our awesome team:

- **Concept, Gameplay & Programming**: [Ethan Navarro](#contact)  
- **Electronics (Arduino, Joystick & Sensors)**: [Lizer Portella](https://www.linkedin.com/in/lizer/)  
- **Art**: [@momophu (Instagram)](https://instagram.com/momophu)  
- **Music**: [@mariacarolinalopez (Instagram)](https://instagram.com/mariacarolinalopez)  
- **UI & Interfaces**: Marc *aka* sopadelletres  

We worked hard AND had a blast! 🤪🕹️

---

## Table of Contents
1. [Game Description](#game-description-%EF%B8%8F)
2. [How to Play](#how-to-play-)
3. [Controls](#controls-)
4. [Arduino & Hardware Setup](#arduino--hardware-setup-)
5. [Art & Music](#art--music-)
6. [Gallery](#gallery-)
7. [Contributing](#contributing-)
8. [Contact](#contact)

---

## Game Description 🎮🦈

**Shark'Eat!** is a **local 1v1** game where:

- One player is the **Shark** trying to eat as many fish as possible. Some fish add points, while others *subtract* (beware of the stinky fish! 💩).
- The other player is a **Bubble Cannon** aiming to stop the shark by **blowing bubbles** (literally! 💨).

After the **Shark Round** ends, **players swap roles**. The former shark becomes the cannon, and vice versa. High score wins! 🎉

<p align="center">
  <img src="https://via.placeholder.com/700x400?text=Gameplay+Screenshot+1" alt="Shark Gameplay" width="700">
</p>

---

## How to Play 🚀

1. **Round 1 (Shark Player)**  
   - Eat as many fish as you can within the time limit!  
   - Avoid negative fish that reduce your score.  
   - **Cannon Player** tries to blow you away by generating bubbles to slow you down.

2. **Round 2 (Swap Roles)**  
   - Now the first-round **Cannon Player** becomes the **Shark** and tries to eat fish.  
   - The first-round **Shark Player** uses the bubble cannon to fend off the new shark.

3. **Score Check**  
   - The total fish you devour minus negative catches determines your final score.  
   - May the hungr... I mean **best** shark win! 🏆🐟

---

## Controls 🕹️

Originally, **Shark’Eat!** was designed with **unique custom controllers** (no standard keyboard/mouse/gamepad! 😎). 

### Original Custom Setup
1. **Shark Controller**  
   - A **3D-printed fish** (yes, a real fish model!) containing a **gyroscope/accelerometer**.  
   - **Rotate the physical fish** to move the on-screen shark.
2. **Bubble Cannon**  
   - A **custom analog joystick** for horizontal cannon aim.  
   - A **microphone** to blow into (💨) and generate bubbles.

### Public Demo Setup
For a more accessible public demo, we’ve added **keyboard controls**:
- **Shark**: Use **Arrow Keys** (↑ ↓ ← →) to move.  
- **Cannon**: Use **A** and **D** to move left/right and **blow into the mic** for bubbles.  

*(Yes, it’s not as crazy as flailing around a 3D fish, but it works in a pinch!) 🤪*

---

## Arduino & Hardware Setup ⚙️

Inside this repository, you’ll find the **Arduino code** that handles inputs from:
- A **joystick** (analog control).
- An **MPU (gyroscope/accelerometer)**.
- A **microphone** (for the bubble-blowing mechanic).

### Components Required
- **Arduino board** (e.g., Arduino UNO).  
- **Joystick** (any analog stick would do).  
- **Gyroscope/Accelerometer** (e.g., MPU-6050).  
- **Microphone** module (to detect blowing).  
- A **3D-printed fish** shell (for the shark controller), or any handheld container to mount the sensor.  
- A stable stand or handle to mount the **joystick** for the bubble cannon.

### Wiring & Code
1. **Clone this repo** or download the **SharkEat.ino** file.  
2. Connect the **joystick** to the Arduino’s analog inputs as per the code references.  
3. Wire up the **MPU** (SDA, SCL, etc.) and the **microphone** input.  
4. Upload the code to your Arduino board.  
5. Launch the **Shark’Eat!** game on your PC and link the Arduino inputs with the game’s control script.
6. On the bottom-left corner of the main menu screen type the serial port.
7. In settings turn "use gyroscope" to true.

<p align="center">
  <img src="https://via.placeholder.com/600x400?text=Arduino+Setup" alt="Arduino Setup" width="600">
</p>

---

## Art & Music 🎨🎵

All art, from sharks to fishies to UI elements, was **created by our team**! Special thanks to:  
- **@momophu (Instagram)** for all the 2D and 3D art assets! 🖌️  
- **@mariacarolinalopez (Instagram)** for the original soundtrack! 🎶  

<p align="center">
  <img src="https://via.placeholder.com/600x400?text=Art+Concepts" alt="Art Concepts" width="600">
</p>

---

## Gallery 📸
Check out a few more shots of our wacky prototypes and in-game madness:

1. **3D-printed fish controller**  
   <p align="center">
     <img src="https://via.placeholder.com/400x300?text=3D+Printed+Fish" alt="3D Printed Fish" width="400">
   </p>

2. **Bubble Cannon joystick + mic**  
   <p align="center">
     <img src="https://via.placeholder.com/400x300?text=Bubble+Cannon+Joystick" alt="Bubble Cannon Joystick" width="400">
   </p>

3. **In-game scoreboard & UI**  
   <p align="center">
     <img src="https://via.placeholder.com/400x300?text=Scoreboard+UI" alt="Scoreboard UI" width="400">
   </p>

*(We might have an unhealthy obsession with fish...)* 🐠🐡🐟

---
## Important Notice

This repository contains the source cod for **Shark'Eat**, originally created during [Global Game Jam 2025]. The primary purpose of making this code available is **for educational and reference use only**.

- **All rights reserved.**  
- **No permission is granted** to use, copy, modify, merge, publish, distribute, sublicense, or sell copies of this software or any of its assets.  
- For inquiries about special permissions or licensing, please contact Ethan at ethangamepublish@gmail.com.
## License

Please refer to the [LICENSE](LICENSE) file in this repository for more information on your rights to view and study this source code.

---

## Contact

- **Main Dev & Concept**: [Ethan Navarro](https://www.linkedin.com/in/ethan-navarro-2911a1196/)  
- **Electronics**: [Lizer Portella](https://www.linkedin.com/in/lizer/)  
- **Artist**: [@momophu](https://instagram.com/momophu)  
- **Musician**: [@mariacarolinalopez](https://instagram.com/mariacarolinalopez)  
- **UI & Interfaces**: *Marc aka sopadelletres*  

Feel free to ping us for any questions or feedback!

---

<p align="center">
  <img src="https://via.placeholder.com/800x400?text=Team+Photo" alt="Team Photo" width="800">
</p>

> Made with ❤️, a bucket of 🦈, a pinch of 🤯, and a whole lotta laughs at the **Global Game Jam 2025**!  

**Enjoy & Happy Sharking!** 🦈🍴  
