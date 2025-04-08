import React, { useState, useEffect } from 'react';
import axios from 'axios';
import {
  Container,
  Typography,
  TextField,
  Button,
  Box,
  Select,
  MenuItem,
  InputLabel,
  FormControl,
  Fade,
  CircularProgress,
} from '@mui/material';

function IssueForm({ token }) {
  const [newIssue, setNewIssue] = useState({
    title: '',
    description: '',
    priority: 0,
    projectId: '',
  });
  const [projects, setProjects] = useState([]);
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    const fetchProjects = async () => {
      try {
        const response = await axios.get('https://localhost:7271/api/Project', {
          headers: { Authorization: `Bearer ${token}` },
        });
        console.log('Projeler:', response.data);
        setProjects(response.data || []);
        if (response.data.length > 0) {
          setNewIssue((prev) => ({ ...prev, projectId: response.data[0].id }));
        }
      } catch (error) {
        console.error('Proje çekme hatası:', error.response ? error.response.data : error.message);
      }
    };
    if (token) fetchProjects();
  }, [token]);

  const handleCreateIssue = async () => {
    setLoading(true);
    console.log('Gönderilen veri:', newIssue);
    try {
      await axios.post('https://localhost:7271/api/Issue', newIssue, {
        headers: { Authorization: `Bearer ${token}` },
      });
      alert('Destek talebi oluşturuldu!');
      setNewIssue({ title: '', description: '', priority: 0, projectId: projects[0]?.id || '' });
    } catch (error) {
      console.error('Talep oluşturma hatası:', error.response ? error.response.data : error.message);
      alert('Bir hata oluştu!');
    } finally {
      setLoading(false);
    }
  };

  return (
    <Container maxWidth="sm" sx={{ mt: 3 }}>
      <Fade in={true} timeout={500}>
        <Box>
          <Typography variant="h6" sx={{ mb: 2, fontWeight: 'bold', color: 'text.primary' }}>
            Yeni Destek Talebi
          </Typography>
          <TextField
            label="Başlık"
            value={newIssue.title}
            onChange={(e) => setNewIssue({ ...newIssue, title: e.target.value })}
            fullWidth
            margin="normal"
            variant="outlined"
            disabled={loading}
            sx={{
              '& .MuiOutlinedInput-root': {
                borderRadius: 2,
                '&:hover fieldset': { borderColor: 'primary.main' },
                '&.Mui-focused fieldset': { borderColor: 'primary.main' },
              },
            }}
          />
          <TextField
            label="Açıklama"
            value={newIssue.description}
            onChange={(e) => setNewIssue({ ...newIssue, description: e.target.value })}
            fullWidth
            margin="normal"
            multiline
            rows={4}
            variant="outlined"
            disabled={loading}
            sx={{
              '& .MuiOutlinedInput-root': {
                borderRadius: 2,
                '&:hover fieldset': { borderColor: 'primary.main' },
                '&.Mui-focused fieldset': { borderColor: 'primary.main' },
              },
            }}
          />
          <FormControl fullWidth margin="normal" disabled={loading}>
            <InputLabel id="priority-label">Öncelik</InputLabel>
            <Select
              labelId="priority-label"
              value={newIssue.priority}
              onChange={(e) => setNewIssue({ ...newIssue, priority: parseInt(e.target.value) })}
              label="Öncelik"
              sx={{ borderRadius: 2 }}
            >
              <MenuItem value={0}>Düşük</MenuItem>
              <MenuItem value={1}>Orta</MenuItem>
              <MenuItem value={2}>Yüksek</MenuItem>
              <MenuItem value={3}>Kritik</MenuItem>
            </Select>
          </FormControl>
          <FormControl fullWidth margin="normal" disabled={loading}>
            <InputLabel id="project-label">Proje</InputLabel>
            <Select
              labelId="project-label"
              value={newIssue.projectId}
              onChange={(e) => setNewIssue({ ...newIssue, projectId: Number(e.target.value) })}
              label="Proje"
              sx={{ borderRadius: 2 }}
            >
              {projects.map((project) => (
                <MenuItem key={project.id} value={project.id}>
                  {project.name}
                </MenuItem>
              ))}
            </Select>
          </FormControl>
          <Button
            variant="contained"
            color="primary"
            onClick={handleCreateIssue}
            fullWidth
            disabled={loading || !newIssue.title || !newIssue.projectId}
            sx={{
              mt: 2,
              py: 1.5,
              borderRadius: 2,
              textTransform: 'none',
              fontWeight: 'bold',
              '&:hover': { bgcolor: 'primary.dark' },
            }}
          >
            {loading ? <CircularProgress size={24} sx={{ color: 'white' }} /> : 'Gönder'}
          </Button>
        </Box>
      </Fade>
    </Container>
  );
}

export default IssueForm;