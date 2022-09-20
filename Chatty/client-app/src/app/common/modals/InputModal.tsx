import { InputGroup, Input, Button, Text } from "@chakra-ui/react";
import { observer } from "mobx-react-lite"
import { useState } from "react";
import { useStore } from "../../stores/store";

interface Props {
    description: string;
    buttonText: string;
    buttonColor?: string;
    inputType?: string;
    handleSubmit: (value: string) => void;
}

export default observer(function InputModal({ description, buttonText, buttonColor = 'blue.600', inputType = 'text', handleSubmit }: Props) {

    const [value, setValue] = useState('');

    const { modalStore } = useStore();

    const handleClick = () => {
        if (value === '') return;

        handleSubmit(value);

        modalStore.closeModal();
    }

    return (
        <>
            <Text>
                {description}
            </Text>
            <InputGroup p='1'>
                <Input
                    value={value}
                    onChange={e => setValue(e.target.value)}
                    width='100%' borderRightRadius='0'
                    type={inputType}
                />
                <Button
                    bgColor={buttonColor}
                    onClick={handleClick}
                    borderLeftRadius='0'
                >
                    {buttonText}
                </Button>
            </InputGroup>
        </>
    )
})