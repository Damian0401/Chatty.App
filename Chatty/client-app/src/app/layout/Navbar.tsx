import { CalendarIcon, ChatIcon, ChevronDownIcon, CloseIcon, HamburgerIcon } from "@chakra-ui/icons";
import { Button, Flex, HStack, Icon, IconButton, Menu, MenuButton, MenuItem, MenuList, Spacer, Text } from "@chakra-ui/react";
import { observer } from "mobx-react-lite";
import { Link } from "react-router-dom";
import { useStore } from "../stores/store";



export default observer(function Navbar() {

    const { userStore } = useStore();

    return (
        <>
            <Flex p='3' bgColor='blackAlpha.700' fontFamily='fantasy'>
                <HStack as={Link} to='/chat' color='whiteAlpha.800'>
                    <Icon as={CalendarIcon} w='8' h='8' />
                    <Text fontSize='3xl' noOfLines={1}>
                        Chattty App
                    </Text>
                </HStack>
                <Spacer />
                    <Menu>
                        <MenuButton
                            as={Button}
                            aria-label='Options'
                            rightIcon={<ChevronDownIcon />}
                            variant='ghost'
                            boxShadow='base'
                            color='whiteAlpha.800'
                            fontFamily='sans-serif'
                            _hover={{bgColor: 'whiteAlpha.300'}}
                            _active={{bgColor: 'whiteAlpha.300'}}
                        >
                            {userStore.user?.userName || '{userName}'}
                        </MenuButton>
                        <MenuList>
                            <MenuItem icon={<CloseIcon />} onClick={() => userStore.logout()}>
                                Logout
                            </MenuItem>
                        </MenuList>
                    </Menu>
            </Flex>
        </>
    )
})