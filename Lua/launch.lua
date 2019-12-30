print('================')

print 'hello world'

local cubePrefab = CS.UnityEngine.Resources.Load("Prefabs/Cube")

local cube = CS.UnityEngine.Object.Instantiate(cubePrefab)
-- cast(cube, typeof(CS.UnityEngine.GameObject))

local type = typeof(CS.UnityEngine.MeshRenderer)

print('===' .. type:ToString())

local renderer = cube:GetComponent(type)

local k = {}
CS.LuaBehaviour.Bind(cube, k)

k.test1.enabled = false

print('================')


local core = require('core')
core.start()

