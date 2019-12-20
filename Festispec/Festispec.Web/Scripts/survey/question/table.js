class TableQuestion {

	constructor(tableId, inputName) {
		this.scope = document.querySelector("#"+ tableId);
		this.inputField = document.querySelector("input[name='" + inputName + "']");
		this.addListners();
	}

	addListners() {
		this.scope.addEventListener('change', (event) => {
			let tableAnswerRows = this.scope.querySelectorAll("tbody tr");
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
			this.setNewValues(JSON.stringify(rowAnswers));
		});
	}

	setNewValues(value) {
		this.inputField.value = value;
	}
}