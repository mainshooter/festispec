class TableQuestion {

	constructor(tableId, inputName) {
        this.scope = document.querySelector("#" + tableId);
        this.table = this.scope.querySelector("table");
		this.inputField = document.querySelector("input[name='" + inputName + "']");
		this.addListners();
    }

    getValues() {
        let tableAnswerRows = this.table.querySelectorAll("tbody tr");
        let rowAnswers = [];
        tableAnswerRows.forEach((row) => {
            let tds = row.querySelectorAll("td");
            let rowValues = [];
            tds.forEach((tdElement) => {
                let inputElement = tdElement.firstElementChild;
                if (inputElement.hasAttribute("type")) {
                    // Is it a text field
                    let value = inputElement.value;
                    rowValues.push(value);
                }
                else {
                    // It is a select
                    let selectedValue = inputElement.querySelector("option:checked");
                    if (selectedValue == null) {
                        rowValues.push("");
                    }
                    else {
                        rowValues.push(selectedValue.value);
                    }
                }
            });
            rowAnswers.push(rowValues);
        });
        return rowAnswers;
    }

	addListners() {
        this.table.addEventListener('change', (event) => {
            this.setNewValues(JSON.stringify(this.getValues()));
        });
        let event = new Event('change');
        this.table.dispatchEvent(event);
    }

    displayNotCompleted() {
        this.scope.querySelector(".table-error").innerHTML = "Niet goed ingevuld";
    }

	setNewValues(value) {
		this.inputField.value = value;
	}
}