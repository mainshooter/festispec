﻿(function () {

})();

function test() {
	var kvpairs = [];
	var form = document.querySelector("form");
for (var i = 0; i < form.elements.length; i++) {
	var e = form.elements[i];
	console.log(e.type);
	if (e.type == "file") {
		let reader = new FileReader();
		let files = e.files;
		for (var j = 0; j < files.length; j++) {
			let file = files[j];
			reader.readAsDataURL(file);
			reader.onloadend = function (evt) {
				console.log(evt.target.result);
				console.log(reader.result);
			}
			reader.onerror = function (error) { console.log(error); }
		}

	}
		kvpairs.push(encodeURIComponent(e.name) + "=" + encodeURIComponent(e.value));
	}
	console.log(kvpairs);
}



(function ($) {
	$.fn.serializeFiles = function () {
		var form = $(this),
			formData = new FormData()
		formParams = form.serializeArray();

		$.each(form.find('input[type="file"]'), function (i, tag) {
			$.each($(tag)[0].files, function (i, file) {
				formData.append(tag.name, file);
			});
		});

		$.each(formParams, function (i, val) {
			formData.append(val.name, val.value);
		});

		return formData;
	};
})(jQuery);

(function () {
	let storage = window.localStorage;
	$("form").submit((event) => {
		event.preventDefault();
		if ($("form").valid({
			debug: true,
		})) {
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
			cases.push(result);
		}
		else {
			surveyCases[surveyId] = [];
			let cases = surveyCases[surveyId];
			cases.push(result);
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