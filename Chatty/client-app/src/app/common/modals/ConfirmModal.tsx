import { InputGroup, Input, Button, Text, ButtonGroup, Flex, Spacer } from "@chakra-ui/react";
import { observer } from "mobx-react-lite"
import { useState } from "react";
import { useStore } from "../../stores/store";

interface Props {
    description: string;
    buttonText: string;
    buttonColor?: string;
    handleSubmit: () => void;
}

export default observer(function ConfirmModal({ description, buttonText, buttonColor = 'red', handleSubmit }: Props) {

    const { modalStore } = useStore();

    const handleClick = () => {
        handleSubmit();
        modalStore.closeModal();
    }

    return (
        <>
            <Text p='1'>
                {description}
            </Text>
            <Flex>
                <Spacer />
                <ButtonGroup size='md'>
                    <Button
                        bgColor={buttonColor}
                        onClick={handleClick}
                    >
                        {buttonText}
                    </Button>
                    <Button onClick={() => modalStore.closeModal()}>
                        Cancel
                    </Button>
                </ButtonGroup>
            </Flex>
        </>
    )
})