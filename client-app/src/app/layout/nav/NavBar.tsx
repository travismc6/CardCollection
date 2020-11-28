import React from 'react';
import { Menu, Container, Button } from 'semantic-ui-react';
import { NavLink } from 'react-router-dom';

const NavBar: React.FC = () => {
  return (
    <Menu fixed='top' inverted>
      <Container>
        <Menu.Item header as={NavLink} exact to='/'>      
          Card Collection
        </Menu.Item>
        <Menu.Item name='My Collection' as={NavLink} to='/activities' />
        <Menu.Item name='Checklist' as={NavLink} to='/activities' />

        <Menu.Item>
          <Button
            as={NavLink} to='/createActivity'
            positive
            content='Add Card'
          />
        </Menu.Item>
      </Container>
    </Menu>
  );
};

export default NavBar;
