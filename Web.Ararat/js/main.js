// data-shared-size-group="panel" data-shared-size="height"


(function () {
	var self = this;

    function alignHeight(group) {
        var elements = Enumerable.From(group.elements);

        var maxHeight = elements.Max(function (x) {
            return $(x).height();
        });


        $(group.elements).height(maxHeight);
    }

    function align(group) {

        var alignType = $(group.elements[0]).data('shared-size');

        switch (alignType) {
        case "height":
            {
                alignHeight(group);
            };
            break;
        default:
            {};
            break;
        }
    }

    function alignGroups(groups) {
        $(groups).each(function (i, e) {
            align(e);
        });
    }

    function group() {
        var groups = [];

        $(groupElements).each(function (i, e) {
            var groupName = $(e).data("shared-size-group");
            var item;
            if (i === 0) {
                item = {
                    name: groupName,
                    elements: [e]
                };
                groups.push(item);
                return;
            }

            var g = Enumerable.From(groups);
            var existingGroup = g.SingleOrDefault(function (x) {
                return x.name === groupName;
            });

            if (existingGroup == null) {
                item = {
                    name: groupName,
                    elements: [e]
                };
                groups.push(item);
            } else {
                existingGroup.elements.push(e);
            }
        });

        return groups;
    }

    var init = function () {
		self.groupElements = $("*[data-shared-size-group]");
		if (self.groupElements.length === 0) {
			return;
		}

		var groups = group();
		alignGroups(groups);
    };

    $(window).load(function() {
        init();
    });
})();