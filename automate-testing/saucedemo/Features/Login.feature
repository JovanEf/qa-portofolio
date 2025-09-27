Feature: Login Page

    Background:
        Given user navigates to the Login page

    Scenario: Successful login with valid credentials
        When user enters username "standard_user"
        And user enters password "secret_sauce"
        And user clicks the Login button
        Then user should sees the products header

    Scenario: Failed login with invalid credentials
        When user enters username "wrong_user"
        And user enters password "wrong_password"
        And user clicks the Login button
        Then user should sees error message "Username and password do not match any user in this service" on Login page
        And user should remain on the Login page

    Scenario Outline: Login with different user types
        When user enters username "<username>"
        And user enters password "<password>"
        And user clicks the Login button
        Then user should sees the products header

        Examples:
            | username      | password     |
            | standard_user | secret_sauce |
            | problem_user  | secret_sauce |

    Scenario: Locked out user cannot login
        When user enters username "locked_out_user"
        And user enters password "secret_sauce"
        And user clicks the Login button
        Then user should sees error message "Sorry, this user has been locked out." on Login page
        And user should remain on the Login page

    Scenario: Login with empty username
        When user enters username ""
        And user enters password "secret_sauce"
        And user clicks the Login button
        Then user should sees error message "Username is required" on Login page
        And user should remain on the Login page

    Scenario: Login with empty password
        When user enters username "standard_user"
        And user enters password ""
        And user clicks the Login button
        Then user should sees error message "Password is required" on Login page
        And user should remain on the Login page

    Scenario: Login with both fields empty
        When user enters username ""
        And user enters password ""
        And user clicks the Login button
        Then user should sees error message "Username is required" on Login page
        And user should remain on the Login page

    Scenario: Login with whitespace only username
        When user enters username "   "
        And user enters password "secret_sauce"
        And user clicks the Login button
        Then user should sees error message "Username and password do not match any user in this service" on Login page
        And user should remain on the Login page

    Scenario: Login with whitespace only password
        When user enters username "standard_user"
        And user enters password "   "
        And user clicks the Login button
        Then user should sees error message "Username and password do not match any user in this service" on Login page
        And user should remain on the Login page

    Scenario: Login with uppercase username
        When user enters username "STANDARD_USER"
        And user enters password "secret_sauce"
        And user clicks the Login button
        Then user should sees error message "Username and password do not match any user in this service" on Login page
        And user should remain on the Login page

    Scenario: Login with uppercase password
        When user enters username "standard_user"
        And user enters password "SECRET_SAUCE"
        And user clicks the Login button
        Then user should sees error message "Username and password do not match any user in this service" on Login page
        And user should remain on the Login page

    Scenario: Login with leading and trailing spaces in username
        When user enters username "  standard_user  "
        And user enters password "secret_sauce"
        And user clicks the Login button
        Then user should sees error message "Username and password do not match any user in this service" on Login page
        And user should remain on the Login page

    Scenario: Login with leading and trailing spaces in password
        When user enters username "standard_user"
        And user enters password "  secret_sauce  "
        And user clicks the Login button
        Then user should sees error message "Username and password do not match any user in this service" on Login page
        And user should remain on the Login page

    Scenario: Multiple login attempts until success
        When user enters username ""
        And user enters password "secret_sauce"
        And user clicks the Login button
        Then user should sees error message "Username is required" on Login page
        When user enters username "standard_user"
        And user clicks the Login button
        Then user should sees the products header