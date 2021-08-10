import { localize,extend } from 'vee-validate';
import zh_CN from 'vee-validate/dist/locale/zh_CN.json';
import * as rules from 'vee-validate/dist/rules'

localize('zh_Cn', zh_CN);
Object.keys(rules).forEach(rule => {
  extend(rule, rules[rule])
})

