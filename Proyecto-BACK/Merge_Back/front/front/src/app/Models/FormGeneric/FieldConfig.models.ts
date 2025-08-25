// export interface FieldConfig {
//     name: string;              // nombre del campo
//     label: string;             // texto visible
//     type: 'text' | 'email' | 'textarea' | 'password' | 'select';  // tipo input
//     required?: boolean;        // si es obligatorio
//     options?: { value: any; label: string }[];
// }


export interface FieldOption {
    label: string;
    value: any;
}

export interface FieldConfig {
    key: string;
    label: string;
    type?: 'text' | 'email' | 'textarea' | 'password' | 'select';
    options?: FieldOption[]; // ⬅️ importante
}
