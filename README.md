# Application Coding Test Exercise
8 hour coding test where it's requested the implementation of an exercise targeted for children to find out numbers spelled.

# Unity Developer Technical Test

This is an exercise for identifying numbers with infinite statements. Prepare a scene where a number appears as text (e.g., "Five"), randomly chosen.
It should appear with:

* A 2-second entry animation (through code or animation).
* Continue to be displayed for 2 seconds.
* A 2-second exit animation.

Once it has disappeared, through an animation, 3 options with different numbers (visual representation, e.g., "5") will appear.
One of them must be the correct option, relating the text to the numeral, and the other two must be incorrect.
If the correct option is clicked:

* It will be colored green, the options will animate out, and immediately after, the next statement will be shown.
If the selected option is incorrect:
  * The first time, it will be colored red and animate out.
  * The second time, it will be colored red, the correct option will be colored green, they will animate out, and the next statement will be shown.
While an animation is running, interaction with the rest of the options should not be possible.
In one corner of the screen, there will be a small text field showing "Correct" and "Incorrect," indicating how many of each have been made during the current session.

# Requirements
* A Model-View-Controller (MVC) architecture must be used throughout the test.
* Comments should be left explaining the SOLID principles used.
* Comments justifying the use of design patterns should be included.
* The use of unit tests will be positively evaluated.

# Observations
* Anything that enhances the final result will be a plus, but the main evaluation will focus on the code. It is recommended to dedicate the majority of efforts to it.
* The aesthetics of the animations are not relevant; a fade in/out is sufficient.
* Animations can be reused (e.g., all entry/exit animations can be the same).
* Special attention should be paid to when the buttons should be clickable and when they should not be.
* Before concluding the test, it is recommended to interact with all possible combinations, analyzing and improving situations where gameplay is not optimal.
* The inclusion of external assets of any kind is not accepted.
* Required Unity version: 2020.3.
* Estimated time: 7 hours.

# Deliverables
* Link to a public Git repository from which the project can be downloaded and tested (Bitbucket is free).
* A document (maximum 2 pages) within the same Git project that explains the decisions made during the test.
