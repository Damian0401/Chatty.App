import { Modal, ModalCloseButton, ModalContent, ModalOverlay } from "@chakra-ui/react";
import { observer } from "mobx-react-lite";
import { useStore } from "../stores/store";



export default observer(function ModalContainer() {

    const { modalStore: { isOpen, content, closeModal } } = useStore();

    return (
        <Modal isOpen={isOpen} onClose={closeModal}>
            <ModalOverlay />
            <ModalContent>
                <ModalCloseButton />
                {content}
            </ModalContent>
        </Modal>
    )
})