const loginForm = document.getElementById("login-form");
const loginButton = document.getElementById("login-form-submit");
const loginErrorMsg = document.getElementById("login-error-msg");

// When the login button is clicked, the following code is executed
loginButton.addEventListener("click", (e) => {
    // Prevent the default submission of the form
    e.preventDefault();
    // Get the values input by the user in the form fields
    const username = loginForm.username.value;
    const password = loginForm.password.value;

    fetch('/api/Users/Login',{
        method: 'GET',
        headers: {'Content-Type': 'application/json;charset=UTF-8'},
        body: JSON.stringify({
            'userName': username,
            'password': password
        })

    }).then(response => console.log(response)).then(window.location.replace("/api/incidentmanagements/GetVoicePlay/view")).catch(err => console.log(err));
})