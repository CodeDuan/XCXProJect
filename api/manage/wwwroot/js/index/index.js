$(function() {
    if($.cookie('loginuser')==null)
    {
        window.location.href="login.html";
    }

    // 初始化
    $('#menuAccordion').accordion({
        fillSpace: true,
        fit: true,
        border: false,
        animate: false
    });

    $.getJSON("js/index/data.json",function(data){
        $.each(data, function(index, item) {
            var selected = false;
            if (index == 0) {
                selected = true;
            }
            // Accordion 折叠面板
            $('#menuAccordion').accordion('add', {
                title: item.text,
                content: "<ul id='menu_tree_" + item.id + "'></ul>",
                selected: selected
            });
            // 树形菜单
            $('#menu_tree_' + item.id).tree({
                data: item.children,
                onClick: function(node) {
                    if (node.children.length != 0) {
                        return;
                    }
                    // 添加选项卡
                    addTab('tabs', node.text, node.url);
                }
            });
        });
    });
    /*$.post(basePath + 'sys/menu/tree', {type: 1}, function(data) {
        if(data) {
            $.each(data, function(index, item) {
                var selected = false;
                if (index == 0) {
                    selected = true;
                }
                // Accordion 折叠面板
                $('#menuAccordion').accordion('add', {
                    title: item.text,
                    content: "<ul id='menu_tree_" + item.id + "'></ul>",
                    selected: selected
                });
                // 树形菜单
                $('#menu_tree_' + item.id).tree({
                    data: item.children,
                    onClick: function(node) {
                        if (node.children.length != 0) {
                            return;
                        }
                        // 添加选项卡
                        addTab('tabs', node.text, node.url);
                    }
                });
            });
        }
    }, 'json');*/
});

/**
 * 添加标签页面板
 * @param tabsId 标签页 ID
 * @param title 标签页面板的标题文字
 * @param url 加载远程内容来填充标签页面板的 URL
 */
function addTab(tabsId, title, url) {
    var $tabs = $('#' + tabsId);
    console.log( tabsId);
    if ($tabs.tabs('exists', title)){
		$tabs.tabs('select', title);
    }
    else
    {
        $tabs.tabs('add', {
            title: title,
            href: 'url',
            closable: true
        });
    }
    
}
