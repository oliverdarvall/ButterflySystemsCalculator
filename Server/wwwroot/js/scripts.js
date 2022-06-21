let operation = "";
let display = document.getElementById("display");

let value1 = "";
let value2 = "";

let previous_answer = 0;

function reset() {
    operation = null;
    value1 = "";
    value2 = "";
}

function clear_display() {
    display.value = "";
}

function check_operation() {
    if (operation === null) {
        // Clear the display for any new buttons pressed after an answer was fetched
        clear_display();
        reset();

        operation = "";
    }
}

function add_to_display(text) {
    check_operation();

    display.value = (display.value + text).replace("..", ".");
}

function prepare_operation(op, text) {
    check_operation();

    if (operation != "")
        display.value = value1;

    value1 = display.value;

    if (value1 == "") {
        // No values entered yet, so continue from previous answer
        value1 = previous_answer.toString();
        display.value = value1;
    }

    operation = op;

    add_to_display(" " + text + " ");
}

function finalize_operation() {
    value2 = "";

    parts = display.value.split(" ");

    if (parts.length > 0) 
        value2 = parts[parts.length - 1];

    if (value2 == "") {
        value2 = "0";
        add_to_display(value2);
    }
}

function get_credentials() {
    // Get username & password from user/authentication-provider/etc, hash the password, etc
    // but for now, JUCK, return hard-coded credentials
    let username = "butterfly";
    let password_hash = "systems";

    return [username, password_hash];
}

function fetch_result() {
    if (operation == "")
        return;

    finalize_operation();

    add_to_display(" = ?");

    const url = new URL("calculator/" + operation + "/" + value1 + "/" + value2, window.location.href);

    const credentials = get_credentials();

    // If there are credentials, use it for Basic authentication
    if (credentials.length == 2) {
        init = {
            method: "GET",
            headers: { "Authorization": "Basic " + btoa(credentials[0] + ":" + credentials[1]) }
        };
    } else {
        init = { method: "GET" };
    }

    fetch(url, init).then(
        response => response.json()
    ).then(
        result => {
            display.value = result.display;
            reset();

            if (result.success)
                previous_answer = result.value;
            else
                display.classList.add("flash");
        }
    ).catch(
        error => console.log(error)
    );
}

reset();

let buttons = document.querySelectorAll("button");

buttons.forEach(function (btn, key, parent) {
    if (btn.id == "") {
        btn.onclick = (event) => {
            add_to_display(btn.innerText);
        }
    }
});

document.getElementById("btn_clr").onclick = (event) => { reset(); clear_display(); }
document.getElementById("btn_eq").onclick = (event) => { fetch_result(); }

document.getElementById("btn_add").onclick = (event) => { prepare_operation("add", "+"); }
document.getElementById("btn_sub").onclick = (event) => { prepare_operation("subtract", "-"); }
document.getElementById("btn_mul").onclick = (event) => { prepare_operation("multiply", "*"); }
document.getElementById("btn_div").onclick = (event) => { prepare_operation("divide", "/"); }

display.addEventListener("animationend", () => { display.classList.remove("flash") }, false);
