(function () {
	let storage = window.localStorage;
	$("form").submit((event) => {
		event.preventDefault();
		if ($("form").valid()) {
			let formResult = $("form").serializeArray();
			
			saveToLocalStorage(formResult);
		}
		else {
			alert("survey not filled in correctly");
		}
	});

	function getCasesFromLocalStorage() {
		let storageResult = storage.getItem("survey");

		if (storageResult == null) {
			storageResult = {};
		}
		else {
			storageResult = JSON.parse(storageResult);
		}

		return storageResult;
	}

	function saveToLocalStorage(result) {
		let surveyCases = getCasesFromLocalStorage();

		if (surveyCases.hasOwnProperty(surveyId)) {
			let cases = surveyCases[surveyId];
			console.log(cases);
		}
		else {
			surveyCases[surveyId] = [];
			let cases = surveyCases[surveyId];
		}
		localStorage.setItem("survey", JSON.stringify(surveyCases));
	}

	setInterval(() => {
		let allCases = getCasesFromLocalStorage();
		for (surveyCaseKey in allCases) {
			let cases = allCases[surveyCaseKey];
			if (cases == null && cases.length < 0) {
				continue;
			}
			for (var i = 0; i < cases.length; i++) {
				let surveyCase = cases[i];
				uploadSurveyCase(allCases, cases, i, surveyCase);
			}
		}
		localStorage.setItem("survey", JSON.stringify(allCases));
	}, 5000);

	function uploadSurveyCase(allCases, surveyCases, index, currentCase) {
		$.ajax({
			type: "POST",
			data: currentCase,
			success: function (data) {
				surveyCases.splice(index, 1);
				localStorage.setItem("survey", JSON.stringify(allCases));
			},
			error: function (errorMsg) {
				console.log("error");
			}
		});
	}
})();