import OpenAPI from 'openapi-typescript-codegen'
import fs from 'fs'
// import input from './WebApiCore-swagger.json' with { type: "json" }
const loadJSON = (path) => JSON.parse(fs.readFileSync(new URL(path, import.meta.url)))
const input = loadJSON('./WebApiCore-swagger.json')

fs.rmSync('src/apis/WebApiCore', { recursive: true, force: true })

// https://github.com/ferdikoomen/openapi-typescript-codegen/
OpenAPI.generate({
  input, //  : '../../../../src/apis/WebApiCore-swagger.json',
  output: './src/apis/WebApiCore',
  httpClient: 'axios'
})

// works in package.json
// "generate-api": "openapi --input ../../../../src/apis/WebApiCore-swagger.json --output ./src/apis/WebApiCore -c axios"
