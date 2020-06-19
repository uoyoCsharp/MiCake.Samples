export default class FormValidation {

    public isNullOrEmpty(value: string | undefined): boolean {
        return (value === null || value === '' || value === undefined) ? true : false;
    }

    public isMobile(value: string): boolean {
        return /^(?:13\d|14\d|15\d|16\d|17\d|18\d|19\d)\d{5}(\d{3}|\*{3})$/.test(value);
    }

    public isEmail(value: string): boolean {
        return /^[a-z0-9]+([._\\-]*[a-z0-9])*@([a-z0-9]+[-a-z0-9]*[a-z0-9]+.){1,63}[a-z0-9]+$/.test(value);
    }

    public isCarNo(value: string): boolean {
        // 新能源车牌
        const xreg = /^[京津沪渝冀豫云辽黑湘皖鲁新苏浙赣鄂桂甘晋蒙陕吉闽贵粤青藏川宁琼使领A-Z]{1}[A-Z]{1}(([0-9]{5}[DF]$)|([DF][A-HJ-NP-Z0-9][0-9]{4}$))/;
        // 旧车牌
        const creg = /^[京津沪渝冀豫云辽黑湘皖鲁新苏浙赣鄂桂甘晋蒙陕吉闽贵粤青藏川宁琼使领A-Z]{1}[A-Z]{1}[A-HJ-NP-Z0-9]{4}[A-HJ-NP-Z0-9挂学警港澳]{1}$/;
        if (value.length === 7) {
            return creg.test(value);
        } else if (value.length === 8) {
            return xreg.test(value);
        } else {
            return false;
        }
    }

    public isAmount(value: string): boolean {
        //金额，只允许保留两位小数
        return /^([0-9]*[.]?[0-9])[0-9]{0,1}$/.test(value);
    }

    public isNum(value: string): boolean {
        //只能为数字
        return /^[0-9]+$/.test(value);
    }

    public isSpecial(value: string): boolean {
        //是否包含特殊字符
        let regEn = /[`~!@#$%^&*()_+<>?:"{},.\/;'[\]]/im,
            regCn = /[·！#￥（——）：；“”‘、，|《。》？、【】[\]]/im;
        if (regEn.test(value) || regCn.test(value)) {
            return true;
        }
        return false;
    }

    public isIdCard(value: string | any): boolean {
        let idCard = value;
        if (idCard.length == 15) {
            return this.isValidityBrithBy15IdCard(idCard);
        } else if (idCard.length == 18) {
            let arrIdCard = idCard.split("");
            if (this.isValidityBrithBy18IdCard(idCard) && this.isTrueValidateCodeBy18IdCard(arrIdCard)) {
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
    }

    private isTrueValidateCodeBy18IdCard(arrIdCard: string | any): boolean {
        let sum = 0;
        let Wi = [7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2, 1];
        let ValideCode = [1, 0, 10, 9, 8, 7, 6, 5, 4, 3, 2];
        if (arrIdCard[17].toLowerCase() == 'x') {
            arrIdCard[17] = 10;
        }
        for (let i = 0; i < 17; i++) {
            sum += Wi[i] * arrIdCard[i];
        }
        let valCodePosition = sum % 11;
        if (arrIdCard[17] == ValideCode[valCodePosition]) {
            return true;
        } else {
            return false;
        }
    }

    private isValidityBrithBy18IdCard(idCard18: string | any): boolean {
        let year = idCard18.substring(6, 10);
        let month = idCard18.substring(10, 12);
        let day = idCard18.substring(12, 14);
        let temp_date = new Date(year, parseFloat(month) - 1, parseFloat(day));
        if (temp_date.getFullYear() != parseFloat(year) || temp_date.getMonth() != parseFloat(month) - 1 || temp_date.getDate() !=
            parseFloat(day)) {
            return false;
        } else {
            return true;
        }
    }

    private isValidityBrithBy15IdCard(idCard15: string | any): boolean {
        let year = idCard15.substring(6, 8);
        let month = idCard15.substring(8, 10);
        let day = idCard15.substring(10, 12);
        let temp_date = new Date(year, parseFloat(month) - 1, parseFloat(day));

        if (temp_date.getFullYear() != parseFloat(year) || temp_date.getMonth() != parseFloat(month) - 1 || temp_date.getDate() != parseFloat(day)) {
            return false;
        } else {
            return true;
        }
    }
}