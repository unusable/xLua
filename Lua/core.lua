local canvasBinder = {}
local mainUI = {}

local start = function()
    print 'start'

    local canvas = Instantiate("Prefabs/UI/Canvas")
    local main = Instantiate("Prefabs/UI/Main")
    main.transform:SetParent(canvas.transform, false)
    local onClick = main.binder.button.onClick
    local listener
    listener = function ()
        print("ooooooooooooooooooooooo")
        onClick:RemoveListener(listener)
    end
    onClick:AddListener(listener)

end



return {
    start = start
}
