import React, { useState, useEffect } from 'react';
import axios from 'axios';
import {
  Container,
  Typography,
  List,
  ListItem,
  ListItemText,
  Paper,
  Fade,
  Box,
  Chip,
  CircularProgress,
  Alert,
} from '@mui/material';
import { styled, ThemeProvider, createTheme } from '@mui/material/styles';
import { PriorityHigh } from '@mui/icons-material';

const theme = createTheme({
  palette: {
    primary: { main: '#14202B' }, // Derin Heisenberg mavisi
    secondary: { main: '#00E5FF' }, // Parlak neon cyan
    background: { default: '#0A1419', paper: '#1C2A35' }, // Koyu laboratuvar tonları
    text: { primary: '#F0F0F0', secondary: '#00E5FF' }, // Beyaz ve neon vurgu
  },
  typography: {
    fontFamily: "'Orbitron', sans-serif", // Fütüristik Heisenberg fontu
    h5: { fontWeight: 800, color: '#00E5FF', letterSpacing: '2px' },
    body2: { fontWeight: 500, color: '#F0F0F0' },
  },
  components: {
    MuiPaper: {
      styleOverrides: {
        root: {
          background: 'linear-gradient(145deg, #1C2A35 0%, #0A1419 100%)',
          borderRadius: 15,
          border: '2px solid #00E5FF',
          boxShadow: '0 10px 30px rgba(0, 229, 255, 0.25), 0 5px 15px rgba(0, 229, 255, 0.15)',
          transition: 'all 0.4s ease',
          '&:hover': {
            boxShadow: '0 15px 40px rgba(0, 229, 255, 0.4), 0 8px 20px rgba(0, 229, 255, 0.2)',
          },
        },
      },
    },
    MuiListItem: {
      styleOverrides: {
        root: {
          borderRadius: 10,
          margin: '8px 0',
          backgroundColor: '#253541',
          border: '1px solid #00E5FF',
          transition: 'all 0.3s ease',
          '&:hover': {
            backgroundColor: '#2F404D',
            boxShadow: '0 5px 15px rgba(0, 229, 255, 0.3)',
            transform: 'translateY(-2px)',
          },
        },
      },
    },
    MuiChip: {
      styleOverrides: {
        root: {
          borderRadius: 6,
          fontWeight: 600,
          color: '#F0F0F0',
          border: '1px solid #00E5FF',
          transition: 'all 0.3s ease',
          '&:hover': {
            boxShadow: '0 0 10px rgba(0, 229, 255, 0.5)',
          },
        },
      },
    },
    MuiAlert: {
      styleOverrides: {
        root: {
          borderRadius: 10,
          padding: '10px 20px',
          backgroundColor: 'rgba(255, 77, 77, 0.15)',
          border: '1px solid #FF4D4D',
          color: '#F0F0F0',
          boxShadow: '0 0 10px rgba(255, 77, 77, 0.3)',
        },
      },
    },
  },
});

const StyledContainer = styled(Container)(({ theme }) => ({
  marginTop: '40px',
  marginBottom: '40px',
  padding: '20px',
  background: 'linear-gradient(145deg, #0A1419 0%, #14202B 100%)',
  borderRadius: 20,
  boxShadow: '0 10px 40px rgba(0, 229, 255, 0.2)',
}));

function UserIssues({ token }) {
  const [issues, setIssues] = useState([]);
  const [projects, setProjects] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const userId = JSON.parse(atob(token.split('.')[1]))['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];

  useEffect(() => {
    const fetchData = async () => {
      try {
        setLoading(true);
        const [issuesResponse, projectsResponse] = await Promise.all([
          axios.get(`https://localhost:7271/api/Issue/user/${userId}`, { headers: { Authorization: `Bearer ${token}` } }),
          axios.get('https://localhost:7271/api/Project', { headers: { Authorization: `Bearer ${token}` } }),
        ]);
        console.log('Gelen veri (talepler):', issuesResponse.data);
        console.log('Gelen veri (projeler):', projectsResponse.data);
        setIssues(issuesResponse.data || []);
        setProjects(projectsResponse.data || []);
        setError(null);
      } catch (error) {
        console.error('Veri çekme hatası:', error.response ? error.response.data : error.message);
        setError('Talepler yüklenirken bir hata çıktı dayı!');
      } finally {
        setLoading(false);
      }
    };
    if (token && userId) fetchData();
  }, [token, userId]);

  const getStatusChip = (status) => {
    switch (status) {
      case 0: return <Chip label="[Aç]ık" sx={{ bgcolor: '#00E5FF', color: '#14202B' }} size="small" />;
      case 1: return <Chip label="[De]vam" sx={{ bgcolor: '#FFB300', color: '#14202B' }} size="small" />;
      case 2: return <Chip label="[Çö]züldü" sx={{ bgcolor: '#00CC00', color: '#14202B' }} size="small" />;
      case 3: return <Chip label="[Ka]palı" sx={{ bgcolor: '#00CC00', color: '#14202B' }} size="small" />;
      default: return <Chip label="[?] Bilinmeyen" sx={{ bgcolor: '#666666', color: '#F0F0F0' }} size="small" />;
    }
  };

  const getPriorityChip = (priority) => {
    switch (priority) {
      case 0: return <Chip icon={<PriorityHigh />} label="[Dü]şük" sx={{ bgcolor: '#00CC00', color: '#F0F0F0' }} size="small" />;
      case 1: return <Chip icon={<PriorityHigh />} label="[Or]ta" sx={{ bgcolor: '#FFB300', color: '#F0F0F0' }} size="small" />;
      case 2: return <Chip icon={<PriorityHigh />} label="[Yü]ksek" sx={{ bgcolor: '#FF4D4D', color: '#F0F0F0' }} size="small" />;
      case 3: return <Chip icon={<PriorityHigh />} label="[Kr]itik" sx={{ bgcolor: '#FF1A1A', color: '#F0F0F0' }} size="small" />;
      default: return <Chip icon={<PriorityHigh />} label="[?] Bilinmeyen" sx={{ bgcolor: '#666666', color: '#F0F0F0' }} size="small" />;
    }
  };

  const getProjectNameById = (projectId) => {
    const project = projects.find((p) => p.id === projectId);
    return project ? project.name : '[?] Bilinmeyen';
  };

  return (
    <ThemeProvider theme={theme}>
      <StyledContainer maxWidth="lg">
        <Fade in={true} timeout={800}>
          <Paper elevation={3}>
            <Typography variant="h5" sx={{ p: 3, textAlign: 'center' }}>
              [Ta]leplerim
            </Typography>
            {loading ? (
              <Box sx={{ display: 'flex', justifyContent: 'center', py: 5 }}>
                <CircularProgress sx={{ color: '#00E5FF' }} />
              </Box>
            ) : error ? (
              <Alert severity="error" sx={{ m: 3 }}>
                {error}
              </Alert>
            ) : issues.length > 0 ? (
              <List sx={{ p: 2 }}>
                {issues.map((issue) => (
                  <Fade in={true} timeout={1000} key={issue.id}>
                    <ListItem>
                      <ListItemText
                        primary={
                          <Typography variant="body1" sx={{ fontWeight: 600, color: '#00E5FF' }}>
                            {issue.title}
                          </Typography>
                        }
                        secondary={
                          <Box sx={{ display: 'flex', flexDirection: 'column', gap: 1, mt: 1 }}>
                            <Box sx={{ display: 'flex', gap: 1 }}>
                              {getStatusChip(issue.status)}
                              {getPriorityChip(issue.priority)}
                            </Box>
                            <Typography variant="body2">
                              [Pr]oje: {getProjectNameById(issue.projectId)}
                            </Typography>
                            <Typography variant="body2">
                              [Ol]uşturan: {issue.createdByUserName || issue.createdByUserId || '[?] Bilinmeyen'}
                            </Typography>
                          </Box>
                        }
                      />
                    </ListItem>
                  </Fade>
                ))}
              </List>
            ) : (
              <Typography sx={{ p: 3, textAlign: 'center', color: '#F0F0F0' }}>
                Henüz talebin yok dayı!
              </Typography>
            )}
          </Paper>
        </Fade>
      </StyledContainer>
    </ThemeProvider>
  );
}

export default UserIssues;