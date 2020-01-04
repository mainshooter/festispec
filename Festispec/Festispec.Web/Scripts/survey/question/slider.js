$(document).ready(() => {
    let form = document.querySelector(".survey-container");
    let formElements = form.elements;
    for (let i = 0; i < formElements.length; i++) {
        let element = formElements[i];
        let type = element.type;
        if (type == "range") {
            element.addEventListener("change", (event) => {
                let target = event.target;
                let container = target.parentNode;
                container.querySelector(".range-value").innerHTML = target.value;
            });

        }
    }
});